Ext.define('GenForm.model.common.KeyValue', {
    extend: 'Ext.data.Model',

    idProperty: 'Key',

    fields: [
        { name: 'Key',   type: 'string' },
        { name: 'Value', type: 'string' }
    ]
});