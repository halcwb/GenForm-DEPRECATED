Ext.define('GenForm.test.view.LoginWindowTests', {
    describe: 'LoginWindowShould',

    tests: function () {
        var me = this, loginWindow = Ext.create('GenForm.view.user.LoginWindow');

        it('have a username textfield', function () {
            expect(loginWindow.getUserNameField().isFormField).toBeTruthy();
        });

        it('have a password textfield', function () {
            expect(loginWindow.getPasswordField().isFormField).toBeTruthy();
        });

        it('have a login button', function () {
            expect(loginWindow.getLoginButton().isXType('button')).toBeTruthy();
        });

        it('have a environment combobox', function () {
            expect(loginWindow.getEnvironmentField().isFormField).toBeTruthy();
        });

        it('have an add environment button', function () {
            expect(loginWindow.getAddEnvironmentButton().isXType('button')).toBeTruthy();
        });
    }
});