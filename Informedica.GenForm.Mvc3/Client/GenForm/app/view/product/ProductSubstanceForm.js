/**
 * Created by .
 * User: hal
 * Date: 27-4-11
 * Time: 21:42
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.ProductSubstanceForm', {
    extend: 'Ext.form.Panel',
    alias: 'widget.productsubstanceform',

    title:'Stoffen',

    items:[
        {
            defaults: {width: 230},
            defaultType: 'textfield',
            padding: '10x',

            items: [
                {
                    fieldLabel: 'Volgorde',
                    name: 'ordernumber',
                    allowBlank:true
                },
                {
                    fieldLabel: 'Stofnaam',
                    name: 'genericname'
                },
                {
                    fieldLabel: 'Hoeveelheid',
                    name: 'quantity'
                },
                {
                    fieldLabel: 'Eenheid',
                    name: 'unit'
                }
            ]
        },
    ]

})