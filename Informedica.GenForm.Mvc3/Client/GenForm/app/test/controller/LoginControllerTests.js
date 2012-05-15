Ext.define('GenForm.test.controller.LoginControllerTests', {

    describe: 'Login controller should',

    tests: function () {
        var me = this,
            domClicker = Ext.create('GenForm.lib.util.DomClicker', {}),
            loginController;

        me.getLoginController = function () {
            GenForm.application.eventbus.bus.click = null;

            if (!loginController) {
                loginController = me.createLoginController();
            }

            return loginController;
        };

        me.createLoginController = function () {
            return GenForm.application.getController('user.Login');
        };

        me.clickButton = function(button) {
            domClicker.click(button.getEl().dom);
        };

        it('be defined', function () {
            expect(me.getLoginController()).toBeDefined();
        });

        it('have a constructor function for a login window', function () {
           var controller = me.getLoginController();
            expect(controller.getLoginWindow).toBeDefined();
        });

        it('listen to the click register environment event', function () {
            var controller = me.getLoginController(),
                environmentWindow;

            spyOn(controller, 'onClickRegisterEnvironment').andCallFake(function () {});
            //GenForm.application.eventbus.bus.click["window[itemId=\"wndEnvironment\"] button[action=registerEnvironment]"]["user.Login"].length = 0;
            controller.init();
            environmentWindow = controller.getEnvironmentWindow();

            environmentWindow.show();
            me.clickButton(environmentWindow.getRegisterEnvironmentButton());

            expect(controller.onClickRegisterEnvironment).toHaveBeenCalled();
        });

        it('listen to the click login event', function () {
            var controller = me.getLoginController(),
                loginWindow;

            spyOn(controller, 'onClickLogin').andCallFake(function () {});
            controller.init();
            loginWindow = controller.getLoginWindow();

            loginWindow.show();
            me.clickButton(loginWindow.getLoginButton());
            expect(controller.onClickLogin).toHaveBeenCalled();
        });

        it('listen to the click new environment event', function () {
            var controller = me.getLoginController(),
                loginWindow;

            spyOn(controller, 'onClickAddEnvironment').andCallFake(function () {});
            controller.init();
            loginWindow = controller.getLoginWindow();

            loginWindow.show();
            me.clickButton(loginWindow.getAddEnvironmentButton());

            expect(controller.onClickAddEnvironment).toHaveBeenCalled();
        });

        it('register an environment after click register environment', function () {
            var controller = me.getLoginController(),
                environmentWindow;

            spyOn(controller, 'registerEnvironment').andCallFake(function () {});
            controller.init();
            environmentWindow = controller.getEnvironmentWindow();

            environmentWindow.show();
            me.clickButton(environmentWindow.getRegisterEnvironmentButton());

            expect(controller.registerEnvironment).toHaveBeenCalled();
        });
    }
});