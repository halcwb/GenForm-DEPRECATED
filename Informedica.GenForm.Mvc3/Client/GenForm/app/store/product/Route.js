Ext.define('GenForm.store.product.Route', {
    extend: 'Ext.data.Store',
    storeId: 'routestore',

    model: 'GenForm.model.product.Route',
    autoLoad: true,
    
    proxy: {
        type: 'direct',
        directFn: GenForm.server.UnitTest.GetRoutes
    }
});