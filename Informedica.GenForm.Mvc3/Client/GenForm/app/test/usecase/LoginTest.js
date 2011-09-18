Ext.define('GenForm.test.usecase.LoginTest', {
    describe: 'Login tests that',

    tests: function () {
        var me = this,
            loginMessage = "",
            refusalMessage = "Login geweigerd",
            successMessage = "Login succesvol";

        me.getLoginController = function () {
            return GenForm.application.getController('user.Login');
        };

        me.getLoginWindow = function () {
            return Ext.ComponentQuery.query('window[title="GenForm Login"]')[0];
        };

        me.getLoginButton = function () {
            return Ext.ComponentQuery.query('toolbar button[action=login]')[0];
        };

        me.getFormField = function (fieldname) {
            return Ext.ComponentQuery.query('textfield[name=' + fieldname + ']')[0];
        };

        me.setFormField = function (textfield, value) {
            textfield.inputEl.dom.value = value;
            textfield.value = value;
            return true;
        };

        me.clickButton = function (button) {
            button.btnEl.dom.click();
        };

        me.checkLoginMessage = function () {
            var results = Ext.ComponentQuery.query('messagebox');
            if (results.length > 0)
            {
                if (results[0].cfg) {
                    if (results[0].cfg.msg === loginMessage)
                    {
                        Ext.ComponentQuery.query('button[text=OK]')[0].btnEl.dom.click();
                        return true;
                    }
                }
            }
            return false;
        };

        it('There should be a login controller', function () {
            expect(me.getLoginController()).toBeDefined();
        });

        it('The user should see a login window at start up with title GenForm Login', function () {
            var window = me.getLoginWindow();
            expect(window).toBeDefined();
        });

        it('This window should not be closable', function () {
            var window = me.getLoginWindow();
            expect(window.closable === false).toBeTruthy();
        });

        it('Only with a valid username and password, you can log in', function () {
            var button = me.getLoginButton();

            me.clickButton(button);
            loginMessage = refusalMessage;
            waitsFor(me.checkLoginMessage, 'refusal message', GenForm.test.waitingTime);
        });

        it('User can set username and password', function () {
            var userField = me.getFormField('UserName'),
                passwField = me.getFormField('Password');

            me.setFormField(userField, "Invalid");
            me.setFormField(passwField, "Invalid");

            expect(userField.getValue()).toBe("Invalid");
            expect(passwField.getValue()).toBe("Invalid");
        });

        it('If Username or password is invalid, user still cannot login', function () {
            var button = me.getLoginButton();

            me.clickButton(button);
            loginMessage = refusalMessage;
            waitsFor(me.checkLoginMessage, 'refusal message', GenForm.test.waitingTime)
        });

        it('User valid user cannot login in, in an invalid environment', function () {
            var userField = me.getFormField('UserName'),
                passwField = me.getFormField('Password'),
                environment = me.getFormField('Environment');

            me.setFormField(userField, "Admin");
            me.setFormField(passwField, "Admin");
            me.setFormField(environment, "GenFormProduction");

            me.clickButton(me.getLoginButton());
            loginMessage = refusalMessage;
            waitsFor(me.checkLoginMessage, 'unsuccessfull login', GenForm.test.waitingTime);

        });

        it('User can login using a valid name and password', function () {
            var userField = me.getFormField('UserName'),
                passwField = me.getFormField('Password'),
                environment = me.getFormField('Environment');

            me.setFormField(userField, "Admin");
            me.setFormField(passwField, "Admin");
            me.setFormField(environment, "GenFormTest");

            me.clickButton(me.getLoginButton());
            loginMessage = successMessage;
            waitsFor(me.checkLoginMessage, "successfull login", GenForm.test.waitingTime);
        });

        it('After login the logged in user can be retrieved', function () {
            var loggedIn;

            Login.GetLoggedInUser(function (result) {
                loggedIn = result.User;
            });

            waitsFor(function () {
                return loggedIn ? true : false;
            });

            runs(function () {
                expect(loggedIn).toBe('Admin');
            });
        });

    }
});