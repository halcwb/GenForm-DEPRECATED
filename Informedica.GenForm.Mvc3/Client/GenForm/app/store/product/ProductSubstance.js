/**
 * Created by .
 * User: hal
 * Date: 5-5-11
 * Time: 11:53
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.store.product.ProductSubstance', {
    extend: 'Ext.data.Store',
    alias: 'widget.productsubstancestore',
    storeId: 'productsubstancestore',

    model: 'GenForm.model.product.ProductSubstance'
});