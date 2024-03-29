using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "PlayerDetails_", menuName ="Scriptable Objects/Player/Player Details")]
public class PlayerDetailsSO : ScriptableObject
{
    #region Header PLAYER BASE DETAILS
    [Space(10)]
    [Header("PlAYER BASE DETAILS")]
    #endregion

    #region Tooltip
    [Tooltip("Player character name. Karakterin ad�.")]
    #endregion
    public string playerCharacterName;

    #region Tooltip
    [Tooltip("Prefab gamepbject for the player. S�r�kle b�rak yapaca�m�z prefab.")]
    #endregion
    public GameObject playerPrefab;

    #region Tooltip
    [Tooltip("Karakter runtime animasyon holder.")]
    #endregion
    public RuntimeAnimatorController runtimeAnimatorController;


    #region Header HEALTH
    [Space(10)]
    [Header("HEALTH")]
    #endregion

    #region Tooltip
    [Tooltip("Ba�lang�� can. PLayer starting health amount")]
    #endregion
    public int playerHealthAmount;

    #region Header OTHER
    [Space(10)]
    [Header("OTHER")]
    #endregion

    #region Tooltip
    [Tooltip("Player icon sprite to be used in the minimap. minimap i�in s2 sprite s��rkle b�rak.")]
    #endregion
    public Sprite playerMiniMapIcon;

    #region Tooltip
    [Tooltip("PLayer hand sprite")]
    #endregion
    public Sprite playerHandSprite;

    #region Validation
# if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(playerCharacterName), playerCharacterName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerPrefab), playerPrefab);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(playerHealthAmount), playerHealthAmount, false);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerMiniMapIcon), playerMiniMapIcon);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerHandSprite), playerHandSprite);
        HelperUtilities.ValidateCheckNullValue(this, nameof(runtimeAnimatorController), runtimeAnimatorController);
    }
#endif
    #endregion
}
