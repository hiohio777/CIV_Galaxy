using System;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour, IAbility
{
    [SerializeField] private Sprite spriteArtFon, spriteArt, frame;
    [SerializeField] private float _progressInterval = 20; // Интервал
    private float _progress = 0; // Прогресс

    protected ICivilization ThisCivilization { get; private set; }
    public event Action<float> ProgressEvent; // Отображение на экране

    public int Id { get; private set; }
    public bool IsActive { get; set; } = false; // Активен ли(доступна ли способность)
    public bool IsReady { get; protected set; } = false; // Готова ли к использованию
    public Sprite Fon => spriteArtFon;
    public Sprite Art => spriteArt;
    public Sprite Frame => frame;

    //Бонусы
    public float AccelerationBonus { get; set; } = 0; // Бонус скорости работы

    private float ProgressProc => _progress / (_progressInterval / 100); // Прогресс сканирования в процентах

    public virtual void Initialize(int id, ICivilization civilization)
    {
        (this.Id, this.ThisCivilization) = (id, civilization);
        ThisCivilization.ExecuteOnTimeEvent += Civilization_ExecuteOnTimeEvent;

        if (civilization.TypeCiv == TypeCivEnum.Al)
            _progress = UnityEngine.Random.Range(_progressInterval / 2, _progressInterval);

        ProgressEvent?.Invoke(ProgressProc);
    }

    public void Civilization_ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false || IsReady) return;

        _progress += deltaTime * (1 + ThisCivilization.IndustryCiv.Points + AccelerationBonus);
        ProgressEvent?.Invoke(ProgressProc);

        if (_progress > _progressInterval)
        {
            _progress = 0;
            IsReady = true;
            ThisCivilization.ExicuteAbility(this);
        }
    }

    public abstract void ApplyAl(Diplomacy diplomacyCiv);
    public virtual void Apply(ICivilization civilizationTarget)
    {
        ProgressEvent?.Invoke(0);
    }

    // Враг не найден(отсрочка выполнения(чуть сбрасывается готовность скилла))
    protected void DelayExecutionAl()
    {
        Debug.Log("--> DelayExecutionAl <--");
        _progress = _progressInterval / 100 * 80;
    }
}
