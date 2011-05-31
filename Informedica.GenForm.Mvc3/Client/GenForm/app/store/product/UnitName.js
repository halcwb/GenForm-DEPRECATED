/**
 * Created by .
 * User: hal
 * Date: 27-4-11
 * Time: 13:14
 * To change this template use File | Settings | File Templates.
 */

Ext.define('GenForm.store.product.UnitName', {
    extend: 'Ext.data.Store',
    alias: 'widget.unitnamestore',
    storeId: 'unitnamestore',
    
    model: 'GenForm.model.product.UnitName',
    autoLoad: true,
    
    proxy: {
        type: 'direct',
        directFn: Product.GetUnitNames
    }
});