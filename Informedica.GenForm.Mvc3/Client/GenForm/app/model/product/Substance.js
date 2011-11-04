Ext.define('GenForm.model.product.Substance', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
        {name: 'Id', type: 'string' },
        {name: 'Name', type: 'string' }
    ],

    belongsTo: [
        { model: 'GenForm.model.product.SubstanceGroup', name: 'group'}
    ]

});