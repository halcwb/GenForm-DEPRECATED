Ext.define('GenForm.test.view.LoginWindowTests', {
    describe: 'LoginWindowShould',

    tests: function () {
        var loginWindow = Ext.create('GenForm.view.user.LoginWindow');

        it('have a username textfield', function () {
            expect(loginWindow.getUserNameField().isFormField).toBeTruthy();
        });

        it('have a password textfield', function () {
            expect(loginWindow.getPasswordField().isFormField).toBeTruthy();
        });

        it('have a login button', function () {
            expect(loginWindow.getLoginButton().isXType('button')).toBeTruthy();
        });

        it('have an environment combobox', function () {
            expect(loginWindow.getEnvironmentField().isFormField).toBeTruthy();
        });

        it('have an add environment button', function () {
            expect(loginWindow.getAddEnvironmentButton().isXType('button')).toBeTruthy();
        });

        it('update a login model with data from the fields', function () {
            var model = Ext.create('GenForm.model.user.Login', {
                UserName: '',
                Password: '',
                Environment: ''
            });

            loginWindow.getUserNameField().setValue('test');
            loginWindow.getPasswordField().setValue('test');
            loginWindow.getEnvironmentField().value = 'test';

            model = loginWindow.updateModel(model);
            expect(model.data.UserName).toBe('test');
            expect(model.data.Password).toBe('test');
            expect(model.data.Environment).toBe('test');
        });
    }
});