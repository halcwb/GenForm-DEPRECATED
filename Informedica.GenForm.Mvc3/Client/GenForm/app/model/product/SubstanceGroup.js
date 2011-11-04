Ext.define('GenForm.model.product.SubstanceGroup', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
        {name: 'Id', type: 'string' },
        {name: 'Name', type: 'string' }
    ],

    hasMany: [
        { model: 'GenForm.model.product.Substance', name: 'substances'}
    ]

});