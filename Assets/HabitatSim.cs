using UnityEngine;

public enum RoomId { Arrival, Control, Power, Sustenance, Environmental, LifeSupport }
public enum GameStatus { Running, Won, Lost }
public enum ActionType { CleanPanels, ReplaceCover, MaintainRecycler, RepairDome, Hydrate, Harvest, Electrolyze, HeatPump }

public class HabitatSim : MonoBehaviour
{
    public static HabitatSim Instance { get; private set; }

    [Header("Time")]
    [SerializeField, Min(1f)] private float secondsPerEarthDay = 20f;
    [SerializeField] private float venusSolarDayDays = 116f;
    [SerializeField] private float daylightDays = 58f;

    [Header("Power (battery bank, 0-100)")]
    [SerializeField] private float solarChargePerDay = 20f;
    [SerializeField] private float baseLoadPerDay = 2f;
    [SerializeField] private float rtgThreshold = 10f;
    [SerializeField] private float rtgPerDay = 1.5f;
    [SerializeField] private float actionPowerCost = 25f;

    [Header("Solar panels")]
    [SerializeField] private float panelWearPerDay = 4f;
    [SerializeField] private float cleanWearRemoved = 50f;
    [SerializeField] private float coverWearRemoved = 100f;

    [Header("Temperature (0-100 gauge)")]
    [SerializeField] private float tempRisePerDay = 2f;
    [SerializeField] private float unpoweredTempRisePerDay = 8f;
    [SerializeField] private float heatPumpDrop = 20f;

    [Header("Oxygen (0-100)")]
    [SerializeField] private float oxygenUsePerDay = 6f;
    [SerializeField] private float unpoweredO2LossPerDay = 60f;
    [SerializeField] private float noWaterO2LossPerDay = 4f;
    [SerializeField] private float electrolysisOutput = 30f;

    [Header("Water (0-100)")]
    [SerializeField] private float recyclerDecayPerDay = 2f;
    [SerializeField] private float recyclerOutputPerDay = 40f;
    [SerializeField] private float crewWaterUsePerDay = 30f;
    [SerializeField] private float hydrateWaterCost = 12f;
    [SerializeField] private float hydrateAmount = 35f;
    [SerializeField] private float harvestAmount = 40f;

    [Header("Crew needs (0-100, 100 = worst)")]
    [SerializeField] private float thirstPerDay = 25f;
    [SerializeField] private float hungerPerDay = 18f;
    [SerializeField] private float needDrainStart = 50f;

    [Header("Dome")]
    [SerializeField] private float coatingDecayPerDay = 1.5f;
    [SerializeField] private float acidDamagePerDay = 20f;

    [Header("Health drains (per Earth day at full severity)")]
    [SerializeField] private float rapidDrainPerDay = 60f;
    [SerializeField] private float heatThreshold = 75f;
    [SerializeField] private float heatDrainMaxPerDay = 10f;
    [SerializeField] private float thirstDrainMaxPerDay = 15f;
    [SerializeField] private float hungerDrainMaxPerDay = 10f;
    [SerializeField] private float criticalOxygen = 20f;

    [Header("Recovery")]
    [SerializeField] private float regenPerDay = 30f;

    public float Power { get; private set; } = 100f;
    public float Oxygen { get; private set; } = 80f;
    public float Water { get; private set; } = 80f;
    public float Temperature { get; private set; } = 30f;
    public float PanelWear { get; private set; }
    public float RecyclerCondition { get; private set; } = 100f;
    public float CoatingCondition { get; private set; } = 100f;
    public float DomeIntegrity { get; private set; } = 100f;
    public float Health { get; private set; } = 100f;
    public float Thirst { get; private set; }
    public float Hunger { get; private set; }
    public float CycleDay { get; private set; }
    public float TotalDays { get; private set; }
    public GameStatus Status { get; private set; } = GameStatus.Running;
    public RoomId? PlayerRoom { get; set; }

    public float SolarIntegrity => 100f - PanelWear;
    public bool Daylight => CycleDay < daylightDays;

    void Awake() => Instance = this;

    void Update()
    {
        if (Status != GameStatus.Running) return;
        Simulate(Time.deltaTime / secondsPerEarthDay);
    }

    void Simulate(float d)
    {
        TotalDays += d;
        CycleDay = (CycleDay + d) % venusSolarDayDays;

        if (Daylight) Power += solarChargePerDay * (SolarIntegrity / 100f) * d;
        Power -= baseLoadPerDay * d;
        if (Power < rtgThreshold) Power += rtgPerDay * d;
        Power = Mathf.Clamp(Power, 0f, 100f);
        bool unpowered = Power <= 0f;

        PanelWear = Mathf.Min(100f, PanelWear + panelWearPerDay * d);

        float tempRise = tempRisePerDay + (unpowered ? unpoweredTempRisePerDay : 0f);
        Temperature = Mathf.Min(100f, Temperature + tempRise * d);

        Oxygen -= (unpowered ? unpoweredO2LossPerDay : oxygenUsePerDay) * d;
        if (Water <= 0f) Oxygen -= noWaterO2LossPerDay * d;
        Oxygen = Mathf.Clamp(Oxygen, 0f, 100f);

        RecyclerCondition = Mathf.Max(0f, RecyclerCondition - recyclerDecayPerDay * d);
        float recycled = unpowered ? 0f : recyclerOutputPerDay * (RecyclerCondition / 100f);
        Water = Mathf.Clamp(Water + (recycled - crewWaterUsePerDay) * d, 0f, 100f);

        CoatingCondition = Mathf.Max(0f, CoatingCondition - coatingDecayPerDay * d);
        float exposure = 1f - CoatingCondition / 100f;
        DomeIntegrity = Mathf.Max(0f, DomeIntegrity - acidDamagePerDay * exposure * d);

        Thirst = Mathf.Min(100f, Thirst + thirstPerDay * d);
        Hunger = Mathf.Min(100f, Hunger + hungerPerDay * d);

        float drain = thirstDrainMaxPerDay * Mathf.InverseLerp(needDrainStart, 100f, Thirst)
                    + hungerDrainMaxPerDay * Mathf.InverseLerp(needDrainStart, 100f, Hunger);

        if (Oxygen <= criticalOxygen) drain += rapidDrainPerDay;
        if (DomeIntegrity <= 0f) drain += rapidDrainPerDay;
        if (Temperature > heatThreshold)
            drain += heatDrainMaxPerDay * Mathf.InverseLerp(heatThreshold, 100f, Temperature);

        if (drain > 0f)
        {
            Health = Mathf.Max(0f, Health - drain * d);
            if (Health <= 0f) Status = GameStatus.Lost;
        }
        else
        {
            Health = Mathf.Min(100f, Health + regenPerDay * d);
        }

        if (Status == GameStatus.Running && TotalDays >= venusSolarDayDays)
            Status = GameStatus.Won;
    }

    public bool Perform(ActionType action)
    {
        if (Status != GameStatus.Running) return false;

        switch (action)
        {
            case ActionType.CleanPanels:
                PanelWear = Mathf.Max(0f, PanelWear - cleanWearRemoved);
                return true;

            case ActionType.ReplaceCover:
                PanelWear = Mathf.Max(0f, PanelWear - coverWearRemoved);
                return true;

            case ActionType.MaintainRecycler:
                RecyclerCondition = 100f;
                return true;

            case ActionType.RepairDome:
                CoatingCondition = 100f;
                return true;

            case ActionType.Hydrate:
                if (Water < hydrateWaterCost) return false;
                Water -= hydrateWaterCost;
                Thirst = Mathf.Max(0f, Thirst - hydrateAmount);
                return true;

            case ActionType.Harvest:
                Hunger = Mathf.Max(0f, Hunger - harvestAmount);
                return true;

            case ActionType.Electrolyze:
                if (Power < actionPowerCost) return false;
                Power -= actionPowerCost;
                Oxygen = Mathf.Min(100f, Oxygen + electrolysisOutput);
                return true;

            case ActionType.HeatPump:
                if (Power < actionPowerCost) return false;
                Power -= actionPowerCost;
                Temperature = Mathf.Max(0f, Temperature - heatPumpDrop);
                return true;
        }
        return false;
    }
}