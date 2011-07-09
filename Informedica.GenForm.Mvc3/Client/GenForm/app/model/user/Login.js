/**
 * Created by .
 * User: hal
 * Date: 29-4-11
 * Time: 13:12
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.model.user.Login',  {
    extend: 'Ext.data.Model',

    associations: [
        { type: 'hasMany', model: 'GenForm.model.validation.ValidationRule', name: 'validationRules'}
    ],

    fields: [
        { name: 'username' , type: 'string' },
        { name: 'password', type: 'string'}
    ],

    checkValidationModelRegistration: function () {
        if (!Ext.ModelManager.getModel('GenForm.model.validation.ValidationRule')) {
            Ext.create('GenForm.model.validation.ValidationRule');
        }
    },

    proxy: {
        type: 'direct',
        directFn: Login.Login2
    }
});