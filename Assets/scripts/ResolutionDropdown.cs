using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionDropdown : MonoBehaviour
{
    public Dropdown resolutionDropdown; // подключаем Dropdown
    private Resolution[] resolutions;

    // Статическая переменная для хранения выбранного разрешения между скриптами
    public static Resolution currentResolution;
    public static int currentResolutionIndex;

    void Start()
    {
        // Получаем все доступные разрешения
        Resolution[] allResolutions = Screen.resolutions;

        // Создаем список уникальных разрешений (ширина x высота)
        List<string> options = new List<string>();
        List<Resolution> uniqueResolutions = new List<Resolution>();
        foreach (Resolution res in allResolutions)
        {
            string option = res.width + " x " + res.height;
            if (!options.Contains(option))
            {
                options.Add(option);
                uniqueResolutions.Add(res);
            }
        }
        resolutions = uniqueResolutions.ToArray();

        // Заполняем Dropdown
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        // Проверяем, сохранено ли разрешение ранее
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            // Если есть сохраненное разрешение - загружаем его
            currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);

            // Проверяем, что индекс не выходит за пределы
            if (currentResolutionIndex >= resolutions.Length)
                currentResolutionIndex = GetMaxResolutionIndex();
        }
        else
        {
            // Если нет сохраненного разрешения - ставим максимальное
            currentResolutionIndex = GetMaxResolutionIndex();
        }

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Устанавливаем текущее разрешение
        currentResolution = resolutions[currentResolutionIndex];

        // Подписываем событие выбора
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // Применяем разрешение с учетом текущего режима
        ApplyResolutionBasedOnMode();
    }

    // Метод для получения индекса максимального разрешения
    private int GetMaxResolutionIndex()
    {
        int maxIndex = 0;
        int maxPixels = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            int totalPixels = resolutions[i].width * resolutions[i].height;
            if (totalPixels > maxPixels)
            {
                maxPixels = totalPixels;
                maxIndex = i;
            }
        }

        Debug.Log($"Максимальное разрешение: {resolutions[maxIndex].width}x{resolutions[maxIndex].height}");
        return maxIndex;
    }

    public void SetResolution(int index)
    {
        currentResolutionIndex = index;
        currentResolution = resolutions[index];

        // Сохраняем выбранное разрешение
        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();

        // Применяем разрешение с учетом текущего режима
        ApplyResolutionBasedOnMode();
    }

    // Метод для применения разрешения с учетом режима экрана
    public void ApplyResolutionBasedOnMode()
    {
        // Всегда устанавливаем выбранное разрешение
        // Unity сама обработает его в зависимости от режима экрана
        Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);

        Debug.Log($"Разрешение установлено: {currentResolution.width}x{currentResolution.height}, FullScreen: {Screen.fullScreen}");
    }

    // Метод для обновления разрешения при смене режима (вызывается из ScreenMode)
    public void RefreshResolution()
    {
        ApplyResolutionBasedOnMode();
    }
}