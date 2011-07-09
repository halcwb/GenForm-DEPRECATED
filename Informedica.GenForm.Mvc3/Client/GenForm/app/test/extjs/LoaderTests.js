Ext.define('GenForm.test.extjs.LoaderTests', {
    describe: 'Ext.Loader',

    tests: function () {
        var createLoginModel, createValidationRuleModel;

        createLoginModel = function () {
            return Ext.create('GenForm.model.user.Login');
        };

        createValidationRuleModel = function () {
            return Ext.create('GenForm.model.validation.ValidationRule');
        };

        it('should be enabled', function () {
            expect(Ext.Loader.config.enabled).toBe(true);
        });

        it('should have a GenForm path', function () {
            expect(Ext.Loader.config.paths.GenForm).toBeDefined();
        });

        it('GenForm.model.user.LoginModel can be created', function () {
            expect(createLoginModel()).toBeDefined();
        });

        it('LoginModel should have validationRules method', function () {
            expect(createLoginModel().validationRules).toBeDefined();
        });

        it('LoginModel should have validationRules after creating ValidationRuleModel', function () {
            createValidationRuleModel();
            expect(createLoginModel().validationRules).toBeDefined();
        });
    }
});