/**
 * Created by .
 * User: hal
 * Date: 6-5-11
 * Time: 13:22
 * To change this template use File | Settings | File Templates.
 */
describe('Ext.Loader', function () {
    var createLoginModel, createValidationRuleModel;

    createLoginModel = function () {
        return Ext.create('GenForm.model.user.LoginModel');
    };

    createValidationRuleModel = function () {
        return Ext.create('GenForm.model.validation.ValidationRuleModel');
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
});