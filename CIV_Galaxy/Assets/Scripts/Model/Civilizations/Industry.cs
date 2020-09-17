﻿using System;

public class Industry
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private const float maxPoint = 1; // Максимальное количество поинтов индустрии
    private const float _progressInterval = 5; // Интервал
    private float _progress = 0; // Прогресс

    private ICivilizationBase _civilization;
    private IndustryData _industryData;

    public Industry() { }

    public void Initialize(ICivilizationBase civilization)
    {
        this._civilization = civilization;
        _industryData = this._civilization.DataBase.Industry;

        _civilization.ExecuteOnTimeEvent += Civilization_ExecuteOnTimeEvent;

        Points = _industryData.Points;
        civilization.CivData.GetIndustryPoints += () => Points;

        _civilization.ExicuteIndustryPoints(Points);
    }

    public void ExicuteIndustryAl()
    {
        // Действия Al
    }

    public bool IsActive { get; set; } = true; // Активен ли

    public float Points { get; set; }

    //Бонусы
    public float AccelerationBonus { get; set; } = 0; // Бонус к скорости роста индустрии(уменьшает интервал между добавлением очков индустрии)

    // Рост индустрии
    private void Civilization_ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false || Points >= maxPoint) return;

        _progress += deltaTime * (_industryData.Acceleration + AccelerationBonus);

        if (_progress > _progressInterval)
        {
            _progress = 0;

            Points += 0.01f;
            _civilization.ExicuteIndustryPoints(Points);
        }
    }
}
