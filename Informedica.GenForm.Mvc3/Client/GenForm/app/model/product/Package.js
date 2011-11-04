Ext.define('GenForm.model.product.Package', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
        {name: 'Id', type: 'string' },
        {name: 'Abbreviation', type: 'string' },
        {name: 'Name', type: 'string' }
    ],

    hasMany: [
        { model: 'GenForm.model.product.Shape', name: 'shapes' }
    ]

});