Ext.define('GenForm.store.product.Unit', {
    extend: 'Ext.data.Store',
    storeId: 'unitstore',
    
    model: 'GenForm.model.product.Unit',
    autoLoad: false,
    
    proxy: {
        type: 'direct',
        directFn: GenForm.server.UnitTest.GetUnits
    }
});