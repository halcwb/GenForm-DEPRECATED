/**
 * Created by .
 * User: hal
 * Date: 5-5-11
 * Time: 11:47
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.model.product.ProductSubstance', {
    extend: 'Ext.data.Model',

    fields: [
        { name: 'OrderNumber', type: 'int' },
        { name: 'GenericName', type: 'string' },
        { name: 'Quantity', type: 'float' },
        { name: 'Unit', type: 'string' }
    ]/*,

    hasMany: 'GenForm.model.product.GenericNameModel'*/
});