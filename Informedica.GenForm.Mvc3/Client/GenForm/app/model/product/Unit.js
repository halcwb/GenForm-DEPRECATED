Ext.define('GenForm.model.product.Unit', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
        { name: 'Id', type: 'string' },
        { name: 'Name', type: 'string' },
        { name: 'Abbreviation', type: 'string' },
        { name: 'Divisor', type: 'float' },
        { name: 'Multiplier', type: 'float' },
        { name: 'IsReference', type: 'boolean' }
    ],

    belongsTo: [
        { model: 'GenForm.model.product.UnitGroup', name: 'unitGroup'}
    ],

    hasMany: [
        { model: 'GenForm.model.product.Shape', name: 'shapes' }
    ]

});