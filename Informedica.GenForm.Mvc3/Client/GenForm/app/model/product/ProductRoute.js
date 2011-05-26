/**
 * Created by .
 * User: hal
 * Date: 7-5-11
 * Time: 6:13
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.model.product.ProductRoute', {
    extend: 'Ext.data.Model',
    alias: 'widget.productroutemodel',

    fields: [ {name: 'RouteName', type: 'string' }],

    proxy: {
        type: 'direct',
        directFn: Product.GetProductRoutes
    }
});