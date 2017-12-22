﻿using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class DetailCouponRepository:IDetailCouponRepository
    {
        private QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        #region Command
        public ChiTietPhieu createDetailCoupon(ChiTietPhieu DetailCouponToCreate)
        {
            _entities.Insert_ChiTietPhieu(DetailCouponToCreate.MaSP, DetailCouponToCreate.MaPhieu, DetailCouponToCreate.DonGia, DetailCouponToCreate.SoLuong);
            _entities.SaveChanges();
            return DetailCouponToCreate;
        }
        public void deleteDetailCouponbyID(String key)
        {
            _entities.DeleteAll_ChiTietPhieu(key);
            _entities.SaveChanges();
        }
        public void deleteDetailCoupon(ChiTietPhieu DetailCouponToDelete)
        {
            _entities.Delete_ChiTietPhieu(DetailCouponToDelete.MaPhieu,DetailCouponToDelete.MaSP);
            _entities.SaveChanges();
        }
        public ChiTietPhieu editDetailCoupon(ChiTietPhieu DetailCouponToEdit)
        {
          //  var originalBill = getDetailCoupon(DetailCouponToEdit.MaPhieu, DetailCouponToEdit.MaSP);
        //    _entities.Entry(originalBill).CurrentValues.SetValues(DetailCouponToEdit);
            _entities.Update_ChiTietPhieu(DetailCouponToEdit.MaSP, DetailCouponToEdit.MaPhieu, DetailCouponToEdit.DonGia, DetailCouponToEdit.SoLuong);
            _entities.SaveChanges();
            return DetailCouponToEdit;
        }
        #endregion
        #region Queries
        public ChiTietPhieu getDetailCoupon(String ID, String key)
        {
            return (from c in _entities.ChiTietPhieux
                    where c.MaPhieu.Equals(ID) && c.MaSP.Equals(key)
                    select c).FirstOrDefault();
        }
        public IEnumerable<ChiTietPhieu> ListDetailCouponByID(String key)
        {
            IEnumerable<ChiTietPhieu> list = _entities.Database.SqlQuery<ChiTietPhieu>("SELECT * FROM ChiTietPhieu WHERE MaPhieu='" + key + "' ").ToList();
            foreach (ChiTietPhieu item in list)
            {
                item.SanPham = (_entities.SanPhams.Where(c => c.MaSP.Equals(item.MaSP)).FirstOrDefault());
                item.PhieuNhapXuat = (_entities.PhieuNhapXuats.Where(c => c.MaPhieu.Equals(item.MaPhieu)).FirstOrDefault());
            }
            return list;
        }
        #endregion
    }
}
