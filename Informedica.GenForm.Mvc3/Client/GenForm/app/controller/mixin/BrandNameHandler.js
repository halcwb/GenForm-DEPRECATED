Ext.define('GenForm.controller.mixin.BrandNameHandler', {

    addBrandToStore: function (brand) {
        var me = this,
            store = me.getBrandStore();

        store.add(brand);
    },

    createBrandNameWindow: function () {
        return Ext.create('GenForm.view.product.BrandNameWindow');
    },

    createEmptyBrand: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.common.IdName');
    },

    onAddBrandName: function () {
        var me = this,
            window = me.getBrandNameWindow(me.createEmptyBrand()).show();

        window.setTitle('Nieuwe merk naam');
        window.show();
    },

    onEditBrandName: function (button) {
        var me = this,
            form = button.findParentByType('productform'),
            brand = form.fields.BrandName.findRecord('Name', form.fields.BrandName.getValue()),
            window = me.getBrandNameWindow(brand);

        window.setTitle('Bewerk merk naam: ' + brand.data.Name);
        window.show();
    },

    getBrand: function (button) {
        return button.findParentByType('brandnamewindow').forms.BrandNameForm.getBrand();
    },

    getBrandStore: function () {
        var window = Ext.ComponentQuery.query('productwindow')[0];
        return window.getProductForm().fields.BrandName.store;
    },

    getBrandNameWindow: function (brand) {
        var me = this, window;

        window = me.createBrandNameWindow();
        if (!brand) {
            me.loadEmptyBrand(window);
        } else {
            window.loadWithBrand(brand);
        }
        return window;
    },

    loadEmptyBrand: function (window) {
        window.loadWithBrand(this.createEmptyBrand());
    },

    onBrandSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('brandnamewindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Brand saved:',  result.data.Name);
            me.addBrandToStore(result.data);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Brand could not be saved: ' + result.message);
        }
    },

    saveBrand: function (button) {
        var me = this,
            brand = me.getBrand(button);
        
        GenForm.server.UnitTest.AddNewBrandName(brand.data, {scope: me, callback:me.onBrandSaved});
    }

});