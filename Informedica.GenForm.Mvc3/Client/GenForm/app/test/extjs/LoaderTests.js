Ext.define('GenForm.test.extjs.LoaderTests', {
    describe: 'Ext.Loader',

    tests: function () {
        var me = this;

        me.createLoginModel = function () {
            return Ext.create('GenForm.model.user.Login');
        };

        me.createValidationRuleModel = function () {
            return Ext.create('GenForm.model.validation.ValidationRule');
        };

        it('should be enabled', function () {
            expect(Ext.Loader.config.enabled).toBe(true);
        });

        it('should have a GenForm path', function () {
            expect(Ext.Loader.config.paths.GenForm).toBeDefined();
        });

        it('GenForm.model.user.LoginModel can be created', function () {
            expect(me.createLoginModel()).toBeDefined();
        });

        //TODO: Don't know why this test does not pass?
        it('LoginModel should have validationRules method', function () {
            expect(me.createLoginModel().validationRules).toBeDefined();
        });

        it('LoginModel should have validationRules after creating ValidationRuleModel', function () {
            me.createValidationRuleModel();
            expect(me.createLoginModel().validationRules).toBeDefined();
        });
    }
});