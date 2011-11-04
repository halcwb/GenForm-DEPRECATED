Ext.define('GenForm.view.product.SubstanceForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.substanceform',

    createItems: function () {
        return [
            { xtype: 'textfield', name:'Name',   fieldLabel: 'Stof Naam', margin: '10 0 10 10' }
        ];
    },

    getSubstance: function () {
        var me = this;
        return me.getFormData();
    }

});