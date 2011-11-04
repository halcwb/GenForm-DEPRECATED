Ext.define('GenForm.model.product.GenericName', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
        { name: 'Id',   type: 'string' },
        { name: 'Name', type: 'string' }
    ]

});