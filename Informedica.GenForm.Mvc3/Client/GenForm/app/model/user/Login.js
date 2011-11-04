Ext.define('GenForm.model.user.Login',  {
    extend: 'Ext.data.Model',
    // This requires is necessary if Ext.Loader is enabled,
    // otherwise this model is not defined
    requires: 'GenForm.model.validation.ValidationRule',

    associations: [
        { type: 'hasMany', model: 'GenForm.model.validation.ValidationRule', name: 'validationRules'}
    ],

    fields: [
        { name: 'Username' , type: 'string' },
        { name: 'Password', type: 'string'},
        { name: 'Environment', type: 'string'}
    ],

    proxy: {
        type: 'direct',
        directFn: GenForm.server.Login.Login
    }
});