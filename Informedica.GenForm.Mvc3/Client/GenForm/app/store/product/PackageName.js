/**
 * Created by .
 * User: hal
 * Date: 27-4-11
 * Time: 13:14
 * To change this template use File | Settings | File Templates.
 */

Ext.define('GenForm.store.product.PackageName', {
    extend: 'Ext.data.Store',
    alias: 'widget.packagenamestore',
    storeId: 'packagenamestore',
    
    model: 'GenForm.model.product.PackageName',
    autoLoad: true,
    
    proxy: {
        type: 'direct',
        directFn: Product.GetPackageNames
    }
});