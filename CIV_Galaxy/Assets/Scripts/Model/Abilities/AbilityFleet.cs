using System;
using UnityEngine;

public class AbilityFleet : MonoBehaviour, IAbility
{
    public event Action<float> ProgressEvent; // Отображение на экране

    [SerializeField] private Sprite spriteArtFon, spriteArt, frame;
    [SerializeField] private float _progressInterval = 20; // Интервал
    private float _progress = 0; // Прогресс

    private ICivilization _civilization;

    public void Initialize(int id, ICivilization civilization)
    {
        this.Id = id;
        this._civilization = civilization;
        _civilization.ExecuteOnTimeEvent += Civilization_ExecuteOnTimeEvent;

        ProgressEvent?.Invoke(ProgressProc);
    }

    public int Id { get; private set; }
    public bool IsActive { get; set; } = false; // Активен ли(доступна ли способность)
    public bool IsReady { get; set; } = false;
    public Sprite Fon => spriteArtFon;
    public Sprite Art => spriteArt;
    public Sprite Frame => frame;

    //Бонусы
    public float AccelerationBonus { get; set; } = 0; // Бонус скорости работы
    private float ProgressProc => _progress / (_progressInterval / 100); // Прогресс сканирования в процентах

    public void Civilization_ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false || IsReady) return;

        _progress += deltaTime * (1 + _civilization.IndustryCiv.Points / 4 + AccelerationBonus);
        ProgressEvent?.Invoke(ProgressProc);

        if (_progress > _progressInterval)
        {
            _progress = 0;
            IsReady = true;
            _civilization.ExicuteAbility(this);
        }
    }

    public void ApplyAl()
    {

    }

    public void Apply()
    {

    }
}
