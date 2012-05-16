Ext.define('GenForm.test.controller.LoginControllerTests', {

    describe: 'Login controller should',

    tests: function () {
        var me = this,
            domClicker = Ext.create('GenForm.lib.util.DomClicker', {}),
            loginController;

        me.getLoginController = function () {
            GenForm.application.eventbus.bus = {};

            if (!loginController) {
                loginController = me.createLoginController();
            }

            return loginController;
        };

        me.createLoginController = function () {
            return GenForm.application.getController('user.Login');
        };

        me.clickButton = function (button) {
            domClicker.click(button.getEl().dom);
        };

        it('be defined', function () {
            expect(me.getLoginController()).toBeDefined();
        });

        it('have a function to get a login window', function () {
            var controller = me.getLoginController();
            expect(controller.getLoginWindow).toBeDefined();
        });

        it('have a function to get an environment window', function () {
            var controller = me.getLoginController();
            expect(controller.getEnvironmentWindow).toBeDefined();
        });

        it('listen to the click login event', function () {
            var controller = me.getLoginController(),
                loginWindow;

            spyOn(controller, 'onClickLogin').andCallFake(function () {
            });
            controller.init();

            loginWindow = controller.getLoginWindow();

            loginWindow.show();
            me.clickButton(loginWindow.getLoginButton());
            expect(controller.onClickLogin).toHaveBeenCalled();
        });

        it('have a loginUser function', function () {
            var controller = me.getLoginController();
            expect(controller.loginUser).toBeDefined();
        });

        it('call the login user function after click login event', function () {
            var controller = me.getLoginController(),
                loginWindow;

            controller.init();
            spyOn(controller, 'loginUser').andCallFake(function () {
                // do nothing
            });
            loginWindow = controller.getLoginWindow();

            loginWindow.show();
            me.clickButton(loginWindow.getLoginButton());
            expect(controller.loginUser).toHaveBeenCalled();
        });

        it('have a user login model', function () {
            var controller = me.getLoginController();
            expect(controller.getUserLoginModel).toBeDefined();
        });

        it('pass an object with username, password and environment to the login user method', function () {
            var controller = me.getLoginController(),
                loginWindow, user = {};

            controller.init();
            controller.loginUser = function (userModel) {
                user = userModel;
            };

            loginWindow = controller.getLoginWindow();

            loginWindow.show();
            me.clickButton(loginWindow.getLoginButton());

            expect(user.UserName).toBeDefined();
            expect(user.Password).toBeDefined();
            expect(user.Environment).toBeDefined();
        });


        it('have an environment store', function () {
            var controller = me.getLoginController(), store;

            controller.init();
            store = controller.getEnvironmentEnvironmentStore();
            expect(store).toBeDefined();
        });

        it('should pass the store to the login window', function () {
            var controller = me.getLoginController(), loginWindow, store;

            controller.init();
            store = controller.getEnvironmentEnvironmentStore();
            loginWindow = controller.getLoginWindow();

            expect(loginWindow.environmentStore === store).toBeTruthy();
        });

        it('update the user login model after the login click event before passing to the login user method', function () {
            var controller = me.getLoginController(),
                loginWindow, user = {};

            controller.init();
            controller.loginUser = function (userModel) {
                user = userModel;
            };

            loginWindow = controller.getLoginWindow();

            loginWindow.show();
            loginWindow.getUserNameField().setValue('Admin');
            loginWindow.getPasswordField().setValue('Admin');
            loginWindow.getEnvironmentField().value = 'Test';

            me.clickButton(loginWindow.getLoginButton());

            expect(user.UserName).toBe('Admin');
            expect(user.Password).toBe('Admin');
            expect(user.Environment).toBe('Test');
        });

        it('should have a login callback function called after logging in', function () {
            var controller = me.getLoginController(), loginWindow, response;

            spyOn(controller, 'loginCallBack').andCallFake(function (result) {
                response = result;
            });
            controller.init();
            loginWindow = controller.getLoginWindow();

            loginWindow.show();
            me.clickButton(loginWindow.getLoginButton());

            waitsFor(function () {
                if (response) return true;
            }, 'response of login call back', GenForm.test.waitingTime);

        });

        it('listen to the click new environment event', function () {
            var controller = me.getLoginController(),
                loginWindow;

            spyOn(controller, 'onClickAddEnvironment').andCallFake(function () {
            });
            controller.init();
            loginWindow = controller.getLoginWindow();

            loginWindow.show();
            me.clickButton(loginWindow.getAddEnvironmentButton());

            expect(controller.onClickAddEnvironment).toHaveBeenCalled();
        });

        it('show an environment window after the click new environment event', function () {
            var controller = me.getLoginController(),
                loginWindow, environmentWindow;

            spyOn(controller, 'onClickAddEnvironment').andCallThrough();
            controller.init();
            loginWindow = controller.getLoginWindow();

            loginWindow.show();
            me.clickButton(loginWindow.getAddEnvironmentButton());

            expect(Ext.ComponentQuery.query('environmentwindow').length).toBe(1);
            environmentWindow = Ext.ComponentQuery.query('environmentwindow')[0];
            environmentWindow.close();
        });

        it('listen to the click register environment event', function () {
            var controller = me.getLoginController(),
                environmentWindow;

            spyOn(controller, 'onClickRegisterEnvironment').andCallFake(function () {
            });
            controller.init();
            environmentWindow = controller.getEnvironmentWindow();

            environmentWindow.show();
            me.clickButton(environmentWindow.getRegisterEnvironmentButton());

            expect(controller.onClickRegisterEnvironment).toHaveBeenCalled();
        });

        it('have a register environment method', function () {
            var controller = me.getLoginController();
            expect(controller.registerEnvironment).toBeDefined();
        });

        it('have a get environment model function', function () {
            expect(me.getLoginController().getEnvironmentEnvironmentModel).toBeDefined();
        });

        it('update the environment model after the click register environment event', function () {
            var controller = me.getLoginController(),
                environmentWindow;

            controller.init();
            environmentWindow = controller.getEnvironmentWindow();
            spyOn(environmentWindow, 'updateModel').andCallFake(function () {
                // Do nothing
            });

            environmentWindow.show();
            me.clickButton(environmentWindow.getRegisterEnvironmentButton());

            expect(environmentWindow.updateModel).toHaveBeenCalled();
        });

        it('register an environment after click register environment', function () {
            var controller = me.getLoginController(),
                environmentWindow;

            controller.init();
            spyOn(controller, 'registerEnvironment').andCallFake(function () {
            });
            environmentWindow = controller.getEnvironmentWindow();

            environmentWindow.show();
            me.clickButton(environmentWindow.getRegisterEnvironmentButton());

            expect(controller.registerEnvironment).toHaveBeenCalled();
        });

        it('call a callback function afte click register environment', function () {
            var controller = me.getLoginController(),
                environmentWindow, response;

            spyOn(controller, 'environmentRegistrationCallBack').andCallFake(function (result) {
                response = result;
            });
            controller.init();
            environmentWindow = controller.getEnvironmentWindow();

            environmentWindow.show();
            me.clickButton(environmentWindow.getRegisterEnvironmentButton());

            waitsFor(function () {
                if (response) return true;
            }, 'response of register environment call back', GenForm.test.waitingTime);
        });

        it('add the new test environment to the store', function () {
            var controller = me.getLoginController(),
                environmentWindow, response;

            spyOn(controller, 'environmentRegistrationCallBack').andCallThrough();
            controller.init();
            environmentWindow = controller.getEnvironmentWindow();

            environmentWindow.getEnvironmentNameField().setValue('New Environment');
            environmentWindow.getDatabaseField().setValue('Data Source=new datasource');

            environmentWindow.show();
            me.clickButton(environmentWindow.getRegisterEnvironmentButton());

            waitsFor(function () {
                return controller.environmentRegistrationCallBack.wasCalled;
            }, 'response of register environment call back', GenForm.test.waitingTime);

            runs(function () {
                expect(controller.getEnvironmentEnvironmentStore().findExact('Name', 'New Environment')).toBeGreaterThan(-1);
            });

        });
    }
});