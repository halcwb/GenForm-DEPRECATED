Ext.define('GenForm.test.controller.LoginControllerTests', {

    describe: 'Login controller should',

    tests: function () {
        var me = this,
            loginController;

        me.getLoginController = function () {
            return loginController || (loginController = me.createLoginController());
        };

        me.createLoginController = function () {
            return GenForm.application.getController('user.Login');
        };

        me.clickButton = function(button) {
          button.btnEl.dom.click();
          console.log('button has been clicked');
        };

        it('be defined', function () {
            expect(me.getLoginController()).toBeDefined();
        });

        it('should have a constructor function for a login window', function () {
           var controller = me.getLoginController();
            expect(controller.getLoginWindow).toBeDefined();
        });

        it('listen to the click login event of the login window', function () {
            var controller = me.getLoginController(),
                loginWindow = controller.getLoginWindow();

            spyOn(controller, 'onClickLogin').andCallFake();

            loginWindow.show();
            me.clickButton(loginWindow.getLoginButton());

            expect(controller.onClickLogin).toHaveBeenCalled();
        });
    }
});