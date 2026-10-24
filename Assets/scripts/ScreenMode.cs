using UnityEngine;
using UnityEngine.UI;

public class ScreenMode : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    private ResolutionDropdown resolutionScript;

    private void Start()
    {
        // Находим скрипт ResolutionDropdown на сцене
        resolutionScript = FindObjectOfType<ResolutionDropdown>();

        // Проверяем, сохранен ли режим ранее
        int savedMode;
        if (PlayerPrefs.HasKey("ScreenMode"))
        {
            // Если есть сохраненный режим - загружаем его
            savedMode = PlayerPrefs.GetInt("ScreenMode", 0);
        }
        else
        {
            // Если нет сохраненного режима - ставим полноэкранный (0)
            savedMode = 0;
        }

        dropdown.value = savedMode;

        // Применяем режим
        OnModeChanged(savedMode);

        // Подписываем событие
        dropdown.onValueChanged.AddListener(OnModeChanged);
    }

    private void OnModeChanged(int index)
    {
        if (index == 0) // Полноэкранный режим
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreen = true;
        }
        else // Оконный режим
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
        }

        // Обновляем разрешение после смены режима
        if (resolutionScript != null)
        {
            // Небольшая задержка для корректного применения
            Invoke("UpdateResolution", 0.1f);
        }

        // Сохраняем выбранный режим
        PlayerPrefs.SetInt("ScreenMode", index);
        PlayerPrefs.Save();

        Debug.Log($"Режим экрана изменен: {(index == 0 ? "Fullscreen" : "Windowed")}");
    }

    private void UpdateResolution()
    {
        if (resolutionScript != null)
        {
            resolutionScript.RefreshResolution();
        }
    }
}