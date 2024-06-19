using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeKeyBindsScript : MonoBehaviour
{
    [SerializeField] private InputActionReference dashAction;

    [SerializeField] private TMP_Text bindingDisplayNameText;

    [SerializeField] private GameObject startRebindObject;

    [SerializeField] private GameObject waitingForInputObject;

    //TODO: pass input manager
    [SerializeField] private PlayerInput playerInput = null;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void StartRebind()
    {
        startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);
        //TODO: inputManager.playerInput.SwitchCurrentActionMap("SettingsPanel");
        dashAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    private void RebindComplete()
    {
        var bindinIndex = dashAction.action.GetBindingIndexForControl(dashAction.action.controls[0]);
        bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            dashAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        //TODO: rebindingOperation.Dispose();
        startRebindObject.SetActive(true);
        waitingForInputObject.SetActive(false);
        //TODO: inputManager.playerInput.SwitchCurrentActionMap("SettingsPanel");
    }
}