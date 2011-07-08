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