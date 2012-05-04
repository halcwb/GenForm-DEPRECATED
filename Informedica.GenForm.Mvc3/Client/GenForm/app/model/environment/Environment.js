Ext.define('GenForm.model.environment.Environment', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
        {name: 'Id', type: 'string'},
        {name: 'Environment', type: 'string'},
        {name: 'Connection', type: 'string'},
        {name: 'LogPath', type: 'string'},
        {name: 'ExportPath', type: 'string'}
    ]
});