using MelonLoader;
using UnityEngine;

// Conditional compilation example for IL2CPP and MONO
// #if <Build config> is used to check the build configuration
#if IL2CPP
using Il2CppScheduleOne.UI.Items;
using Il2CppScheduleOne.ItemFramework;
using Il2CppScheduleOne.DevUtilities;
#elif MONO
using ScheduleOne.UI.Items;
using ScheduleOne.ItemFramework;
using ScheduleOne.DevUtilities;
#endif

[assembly: MelonInfo(typeof(CopyPasteFilterHotkeys.CopyPasteFilterHotkeys), "CopyPasteFilterHotkeys", "0.1.0", "Delassa", null)] // Change this
[assembly: MelonGame("TVGS", "Schedule I")]

namespace CopyPasteFilterHotkeys
{
    public class CopyPasteFilterHotkeys : MelonMod
    {
        private MelonPreferences_Category _category;
        private MelonPreferences_Entry<KeyCode> _copyKey;
        private MelonPreferences_Entry<KeyCode> _pasteKey;
        private SlotFilter _copiedFilter;
        
        public override void OnInitializeMelon()
        {
            _category = MelonPreferences.CreateCategory("CopyPasteFilterHotkeys", "Copy Paste Filter Hotkeys Settings");
            _copyKey = _category.CreateEntry("CopyKey", KeyCode.LeftBracket, "Copy Filter Hotkey");
            _copyKey.Description = "Key to use to copy filter settings";
            _pasteKey = _category.CreateEntry("PasteKey", KeyCode.RightBracket, "Paste Filter Hotkey");
            _pasteKey.Description = "Key to use to paste filter settings";
            LoggerInstance.Msg($"Initialized. Use <{_copyKey.Value}> to copy a filter, and <{_pasteKey.Value}> to paste a filter");
        }

        public override void OnLateUpdate()
        {
            if (Singleton<ItemUIManager>.InstanceExists && Singleton<ItemUIManager>.Instance.HoveredSlot != null)
            {
                if (Input.GetKeyDown(_copyKey.Value))
                {
                    if (Singleton<ItemUIManager>.InstanceExists &&
                        Singleton<ItemUIManager>.Instance.HoveredSlot.assignedSlot.PlayerFilter != null &&
                        Singleton<ItemUIManager>.Instance.HoveredSlot.assignedSlot.CanPlayerSetFilter)
                    {
                        MelonLogger.Msg("Copying filter from hovered item slot");
                        _copiedFilter = Singleton<ItemUIManager>.Instance.HoveredSlot.assignedSlot.PlayerFilter;
                        Singleton<ItemUIManager>.Instance.HoveredSlot.UpdateUI();
                    }
                }

                if (Input.GetKeyDown(_pasteKey.Value))
                {
                    if (Singleton<ItemUIManager>.InstanceExists && 
                        _copiedFilter != null &&
                        Singleton<ItemUIManager>.Instance.HoveredSlot.assignedSlot.CanPlayerSetFilter)
                    {
                        MelonLogger.Msg("Pasting Filter onto hovered item slot.");
                        Singleton<ItemUIManager>.Instance.HoveredSlot.assignedSlot.SetPlayerFilter(_copiedFilter);
                        Singleton<ItemUIManager>.Instance.HoveredSlot.UpdateUI();
                    }
                }
            }
        }
    }
}