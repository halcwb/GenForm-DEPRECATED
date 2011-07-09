Ext.define('GenForm.model.validation.ValidationRule', {
    extend: 'Ext.data.Model',

    fields: [
        { name: 'type', type: 'string'},
        { name: 'field', type: 'string'}
    ],

    associations: [
        { type: 'belongsTo', model: 'GenForm.model.user.Login', name: 'login'}
    ]

});