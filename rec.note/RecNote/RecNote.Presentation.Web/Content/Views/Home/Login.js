var onRecoveryPassword = false;
function userPasswordRecovery() {
    if (Util.isNull($('#Username').val())) {
        alert(i18n.getString('error.userNameNull'));
        return;
    }
    if (onRecoveryPassword) { return; }
    onRecoveryPassword = true;
    Util.post('Home/RecoveryPassword', { mail: $('#Username').val() }, function () { alert(i18n.getString('message.recoveryPasswordSent')); onRecoveryPassword = false; });
}