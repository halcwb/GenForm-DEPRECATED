Ext.define('GenForm.view.product.BrandForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.brandform',

    createItems: function () {
        var me = this;
        return [
            me.createTextField({name:'BrandName',   fieldLabel: 'Merk Naam', margin: '10 0 10 10' })
        ];
    },

    getBrand: function () {
        var me = this;
        return me.getFormData();
    }

});