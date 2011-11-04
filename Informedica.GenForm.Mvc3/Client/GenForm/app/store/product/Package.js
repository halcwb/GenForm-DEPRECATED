Ext.define('GenForm.store.product.Package', {
    extend: 'Ext.data.Store',
    storeId: 'packagenamestore',
    
    model: 'GenForm.model.product.Package',
    autoLoad: false,
    
    proxy: {
        type: 'direct',
        directFn: GenForm.server.UnitTest.GetPackages
    }
});