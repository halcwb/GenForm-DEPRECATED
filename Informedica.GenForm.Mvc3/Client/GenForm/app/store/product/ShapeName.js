/**
 * Created by .
 * User: hal
 * Date: 27-4-11
 * Time: 13:14
 * To change this template use File | Settings | File Templates.
 */

Ext.define('GenForm.store.product.ShapeName', {
    extend: 'Ext.data.Store',
    alias: 'widget.shapenamestore',
    storeId: 'shapenamestore',
    
    model: 'GenForm.model.product.ShapeName',
    autoLoad: true,
    
    proxy: {
        type: 'direct',
        directFn: Product.GetShapeNames
    }
});