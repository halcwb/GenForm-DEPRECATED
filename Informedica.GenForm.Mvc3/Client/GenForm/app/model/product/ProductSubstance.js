Ext.define('GenForm.model.product.ProductSubstance', {
    extend: 'Ext.data.Model',

    fields: [
        { name: 'SortOrder', type: 'int' },
        { name: 'Substance', type: 'string' },
        { name: 'Quantity',  type: 'float' },
        { name: 'Unit',      type: 'string' }
    ]

});