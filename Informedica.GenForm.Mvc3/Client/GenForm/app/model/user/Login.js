Ext.define('GenForm.model.user.Login',  {
    extend: 'Ext.data.Model',
    // This requires is necessary if Ext.Loader is enabled,
    // otherwise this model is not defined
    requires: 'GenForm.model.validation.ValidationRule',

    associations: [
        { type: 'hasMany', model: 'GenForm.model.validation.ValidationRule', name: 'validationRules'}
    ],

    fields: [
        { name: 'username' , type: 'string' },
        { name: 'password', type: 'string'},
        { name: 'database', type: 'string'}
    ],

    proxy: {
        type: 'direct',
        directFn: Login.Login
    }
});