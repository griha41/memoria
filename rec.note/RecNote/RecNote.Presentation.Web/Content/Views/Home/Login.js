
function userPasswordRecovery() {
    if (Util.isNull($('#Username').val())) {
        alert(i18n.getString('error.userNameNull'));
        return;
    }
    Util.post('Home/RecoveryPassword', { mail: $('#Username').val() }, function () { alert(i18n.getString('message.recoveryPasswordSent')); });
}