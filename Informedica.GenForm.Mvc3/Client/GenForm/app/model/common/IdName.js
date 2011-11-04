Ext.define('GenForm.model.common.IdName', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
        { name: 'Id', type: 'string'},
        { name: 'Name', type: 'string'}
    ]
})