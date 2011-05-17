/**
 * Created by .
 * User: hal
 * Date: 27-4-11
 * Time: 15:33
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.model.product.GenericNameModel', {
    extend: 'Ext.data.Model',
    alias: 'widget.genericnamemodel',

    fields: [ {name: 'GenericName', type: 'string' }],

    proxy: {
        type: 'direct',
        directFn: Product.GetGenericNames
    }
})