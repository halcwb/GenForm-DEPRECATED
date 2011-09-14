Ext.define('GenForm.model.environment.Environment', {
    extend: 'Ext.data.Model',

    fields: [
        {name: 'EnvironmentName', type: 'string'},
        {name: 'ConnectionString', type: 'string'}
    ]
});