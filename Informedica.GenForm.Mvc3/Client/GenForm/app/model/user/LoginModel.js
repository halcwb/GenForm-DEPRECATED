/**
 * Created by .
 * User: hal
 * Date: 29-4-11
 * Time: 13:12
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.model.user.LoginModel',  {
    extend: 'Ext.data.Model',

    constructor: function (config) {
        this.fields = this.createFields();
        //this.associations = this.createAssociations();

        return this;
    },

    associations: [
        { type: 'hasMany', model: 'GenForm.model.validation.ValidationRuleModel', name: 'validationRules'}
    ],

    createFields: function () {
        return [
            { name: 'username' , type: 'string' },
            { name: 'password', type: 'string'}
        ]
    },

    createAssociations: function() {
        this.checkValidationModelRegistration();
        return [
            { type: 'hasMany', model: 'GenForm.model.validation.ValidationRuleModel', name: 'validationRules'}
        ]
    },

    checkValidationModelRegistration: function () {
        if (!Ext.ModelManager.getModel('GenForm.model.validation.ValidationRuleModel')) {
            Ext.create('GenForm.model.validation.ValidationRuleModel');
        }
    },

    proxy: {
        type: 'direct',
        directFn: Login.Login2
    }
});