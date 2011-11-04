Ext.define('GenForm.store.product.Substance', {
    extend: 'Ext.data.Store',
    storeId: 'substancestore',
    
    model: 'GenForm.model.product.Substance',
    autoLoad: true,

    proxy: {
        type: 'direct',
        directFn: GenForm.server.UnitTest.GetSubstances
    }

});