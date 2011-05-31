/**
 * Created by .
 * User: hal
 * Date: 27-4-11
 * Time: 13:14
 * To change this template use File | Settings | File Templates.
 */

Ext.define('GenForm.store.product.BrandName', {
    extend: 'Ext.data.Store',
    alias: 'widget.brandnamestore',
    storeId: 'brandnamestore',
    
    model: 'GenForm.model.product.BrandName',
    autoLoad: true,
    
    proxy: {
        type: 'direct',
        directFn: Product.GetBrandNames
    }
});