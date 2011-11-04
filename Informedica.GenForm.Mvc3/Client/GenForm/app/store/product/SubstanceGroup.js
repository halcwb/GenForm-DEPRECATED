Ext.define('GenForm.store.product.SubstanceGroup', {
    extend: 'Ext.data.Store',
    storeId: 'substancegroupstore',
    
    model: 'GenForm.model.product.SubstanceGroup',
    autoLoad: false,
    
    proxy: {
        type: 'direct',
        directFn: GenForm.server.UnitTest.GetSubstanceGroups
    }
});