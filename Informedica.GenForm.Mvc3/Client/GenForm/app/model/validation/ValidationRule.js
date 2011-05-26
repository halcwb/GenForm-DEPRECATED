/**
 * Created by .
 * User: hal
 * Date: 29-4-11
 * Time: 20:07
 * To change this template use File | Settings | File Templates.
 */
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