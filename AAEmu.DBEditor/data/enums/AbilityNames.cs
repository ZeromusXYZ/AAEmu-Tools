using System;
using System.Collections.Generic;
using System.Linq;

namespace AAEmu.DBEditor.data.enums;

public class AbilityNames
{
    public static sbyte GetMaxClassId()
    {
        var max = AbilityTypeKr.General;
        foreach (var value in Enum.GetValues<AbilityTypeKr>())
        {
            if (value == AbilityTypeKr.General)
                continue;
            var check = Data.Server.CompactSqlite.UiTexts.FirstOrDefault(x => x.CategoryId == 49 && x.Key == value.ToString().ToLower());
            if (check != null)
            {
                max = value;
            }
            else
            {
                break;
            }
        }
        return (sbyte)max;
    }

    public static string GetClassName(sbyte ability1, sbyte ability2, sbyte ability3)
    {
        var max = GetMaxClassId();
        var abilityList = new List<sbyte>() { ability1, ability2, ability3 };
        abilityList.Sort();
        var classNames = Data.Server.CompactSqlite.UiTexts.Where(x => x.CategoryId == 86);
        var keyName = $"name_{abilityList[0]}_{abilityList[1]}_{abilityList[2]}";
        if (ability2 <= 0 || ability2 > max || ability3 <= 0 || ability3 > max)
        {
            // Not all 3 classes unlocked
            keyName = $"name_{(AbilityTypeKr)ability1}".ToLower();
        }
        var idx = classNames.FirstOrDefault(x => x.Key == keyName);
        return Data.Server.GetText("ui_texts", "text", idx?.Id ?? 0, idx?.Text ?? "???", true);
    }

    public static string GetClassName(sbyte ability)
    {
        if (ability > GetMaxClassId())
        {
            return "<Out of Bounds>";
        }
        var keyName = $"{(AbilityTypeKr)ability}".ToLower();
        return Data.Server.GetUiText(49, keyName, $"{ability}", true);
        /*
        var classNames = Data.Server.CompactSqlite.UiTexts.Where(x => x.CategoryId == 49);
        var idx = classNames.FirstOrDefault(x => x.Key == keyName);
        return Data.Server.GetText("ui_texts", "text", idx?.Id ?? 0, idx?.Text ?? "???", true);
        */
    }
}