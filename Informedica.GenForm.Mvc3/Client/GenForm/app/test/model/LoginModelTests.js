Ext.define('GenForm.test.model.LoginModelTests', {

    describe: 'GenForm.model.user.Login',

    tests: function () {
        var getLoginModel, model, createLoginModel, setValidationRules;


        createLoginModel = function () {
            return Ext.create('GenForm.model.user.Login');
        };

        getLoginModel = function () {
            if (!Ext.ModelManager.getModel('GenForm.model.user.Login')) {
                createLoginModel();
            }
            return Ext.ModelManager.getModel('GenForm.model.user.Login');
        };

        setValidationRules = function (model) {
            model.validations.push({ type: 'presence', field: 'username'});
            model.validations.push({ type: 'presence', field: 'password'});
        };

        applyValidationRules = function (model) {
            model.validationRules().each(function (rule) {
                model.validations.push({ type: rule.data.type, field: rule.data.field});
            });
        };

        it('UserLoginModel should be registered', function () {
            expect(getLoginModel()).toBeDefined();
        });

        it('LoginModel should be instantiated by Ext.create', function () {
            model = createLoginModel();

            expect(model).toBeDefined();
            model = null;
        });

        it('an instantiated LoginModel should have a method validationRules', function () {
            expect(createLoginModel().validationRules).toBeDefined();
        });

        it('An instance of a LoginModel should have validationrules', function () {
            model = createLoginModel();

            if (model.validations.length === 0) setValidationRules(model);

            expect(model.validations.length).toBe(2);

            model.validations = null;
            model = null;
        });

    }
});